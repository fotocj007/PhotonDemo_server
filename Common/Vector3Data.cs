using System;

namespace Common
{
    //设置为可以序列化
    [Serializable]
    public class Vector3Data
    {
        public float x { get; set; }
        public float y { get; set; }
        public float z { get; set; }
    }
}