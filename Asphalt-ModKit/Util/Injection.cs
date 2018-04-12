using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Asphalt.Api.Util
{
    //You can also call this class magic ;)
    public class Injection
    {
        private const BindingFlags PUBLIC_STATC = BindingFlags.Static | BindingFlags.Public;
        private const BindingFlags PUBLIC_INSTANCE = BindingFlags.Instance | BindingFlags.Public;

        public static void InstallCreateAtomicAction(Type pTypeToReplace, Type pHelperType)
        {
            InstallWithOriginalHelperPublicInstance(pTypeToReplace, pHelperType, "CreateAtomicAction");
        }

        public static void InstallWithOriginalHelperPublicStatic(Type pTypeToReplace, Type pHelperType, string pMethodName)
        {
            Install(
                    pTypeToReplace.GetMethod(pMethodName, PUBLIC_STATC),
                    pHelperType.GetMethod(pMethodName, PUBLIC_STATC),
                    pHelperType.GetMethod(pMethodName + "_original", PUBLIC_STATC)
                 );
        }

        public static void InstallWithOriginalHelperPublicInstance(Type pTypeToReplace, Type pHelperType, string pMethodName)
        {
            Install(
                    pTypeToReplace.GetMethod(pMethodName, PUBLIC_INSTANCE),
                    pHelperType.GetMethod(pMethodName, PUBLIC_INSTANCE),
                    pHelperType.GetMethod(pMethodName + "_original", PUBLIC_INSTANCE)
                 );
        }

        public static void Install(MethodInfo pMethodToReplace, MethodInfo pMethodToInject, MethodInfo pNewLocationForMethodToReplace = null)
        {
            if (!pMethodToReplace.GetParameters().Select(p => p.ParameterType).SequenceEqual(pMethodToInject.GetParameters().Select(p => p.ParameterType)))
                throw new ArgumentException("MethodInfos doesn't have the same parameters");

            RuntimeHelpers.PrepareMethod(pMethodToReplace.MethodHandle);
            RuntimeHelpers.PrepareMethod(pMethodToInject.MethodHandle);

            if (pNewLocationForMethodToReplace != null)
                RuntimeHelpers.PrepareMethod(pNewLocationForMethodToReplace.MethodHandle);

            bool compiledInDebug = IsAssemblyDebugBuild(pMethodToReplace.DeclaringType.Assembly);

            unsafe
            {
                if (IntPtr.Size == 4)
                {
                    int* inj = (int*)pMethodToInject.MethodHandle.Value.ToPointer() + 2;
                    int* tar = (int*)pMethodToReplace.MethodHandle.Value.ToPointer() + 2;

                    if (compiledInDebug)
                    {
                        //             Console.WriteLine("\nVersion x86 Debug\n");

                        byte* injInst = (byte*)*inj;
                        byte* tarInst = (byte*)*tar;

                        int* injSrc = (int*)(injInst + 1);
                        int* tarSrc = (int*)(tarInst + 1);

                        if (pNewLocationForMethodToReplace != null)
                        {
                            int* newloc = (int*)pNewLocationForMethodToReplace.MethodHandle.Value.ToPointer() + 2;
                            byte* newLocInst = (byte*)*newloc;
                            int* newLocSrc = (int*)(newLocInst + 1);

                            *newLocSrc = (((int)tarInst + 5) + *tarSrc) - ((int)newLocInst + 5);
                        }

                        *tarSrc = (((int)injInst + 5) + *injSrc) - ((int)tarInst + 5);
                    }
                    else
                    {
                        //    Console.WriteLine("\nVersion x86 Release\n");

                        if (pNewLocationForMethodToReplace != null)
                        {
                            int* newloc = (int*)pNewLocationForMethodToReplace.MethodHandle.Value.ToPointer() + 2;
                            *newloc = *tar;
                        }

                        *tar = *inj;
                    }
                }
                else
                {

                    long* inj = (long*)pMethodToInject.MethodHandle.Value.ToPointer() + 1;
                    long* tar = (long*)pMethodToReplace.MethodHandle.Value.ToPointer() + 1;

                    if (compiledInDebug)
                    {
                        //           Console.WriteLine("\nVersion x64 Debug\n");
                        byte* injInst = (byte*)*inj;
                        byte* tarInst = (byte*)*tar;

                        int* injSrc = (int*)(injInst + 1);
                        int* tarSrc = (int*)(tarInst + 1);

                        if (pNewLocationForMethodToReplace != null)
                        {
                            long* newloc = (long*)pNewLocationForMethodToReplace.MethodHandle.Value.ToPointer() + 1;
                            byte* newLocInst = (byte*)*newloc;
                            int* newLocSrc = (int*)(newLocInst + 1);

                            *newLocSrc = (((int)tarInst + 5) + *tarSrc) - ((int)newLocInst + 5);
                        }

                        *tarSrc = (((int)injInst + 5) + *injSrc) - ((int)tarInst + 5);
                    }
                    else
                    {
                        //        Console.WriteLine("\nVersion x64 Release\n");

                        if (pNewLocationForMethodToReplace != null)
                        {
                            long* newloc = (long*)pNewLocationForMethodToReplace.MethodHandle.Value.ToPointer() + 1;
                            *newloc = *tar;
                        }

                        *tar = *inj;
                    }
                }
            }
        }


        private static bool IsAssemblyDebugBuild(Assembly assembly)
        {
            foreach (var attribute in assembly.GetCustomAttributes(false))
            {
                var debuggableAttribute = attribute as DebuggableAttribute;
                if (debuggableAttribute != null)
                {
                    return debuggableAttribute.IsJITTrackingEnabled;
                }
            }
            return false;
        }

    }
}
