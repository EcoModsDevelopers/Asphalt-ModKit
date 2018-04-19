/** 
 * ------------------------------------
 * Copyright (c) 2018 [Kronox]
 * See LICENSE file in the project root for full license information.
 * ------------------------------------
 * Created by Kronox on March 27, 2018
 * ------------------------------------
 **/

namespace Asphalt.Storeable
{
    public class GenericStoreable<T>
    {
        private T obj;

        public GenericStoreable(T t)
        {
            this.Set(t);
        } 

        public T Get()
        {
            return this.obj;
        }

        public void Set(T t)
        {
            this.obj = t;
        }
    }
}
