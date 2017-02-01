namespace XMLFeedsViewer.Services.XmlDbFiller
{
    using System.Collections.Generic;
    using XMLFeedsViewer.Data.Common.Models;

    public class XmlEntityEqualityComparer<T> : IEqualityComparer<T>
        where T : class, IXmlInfo
    {
        public bool Equals(T x, T y)
        {
            if (x.XmlId == y.XmlId)
            {
                return true;
            }

            return false;
        }

        public int GetHashCode(T obj)
        {
            int hCode = obj.XmlId ^ obj.ParentXmlId;
            return hCode.GetHashCode();
        }
    }
}
