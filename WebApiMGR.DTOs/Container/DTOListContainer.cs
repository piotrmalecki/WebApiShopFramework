using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiMGR.DTOs.Container
{
    public class DTOListContainer<T>
    {
        public int ResultsCount
        {
            get
            {
                return this.Results.Count;
            }
        }

        public int Total
        {
            get;
            set;
        }
        public bool ShouldSerializeTotal()
        {
            return this.Total > 0;
        }

        public int Page
        {
            get;
            set;
        }
        public bool ShouldSerializePage()
        {
            return this.Page > -1;
        }

        public string Message
        {
            get;
            set;
        }
        public bool ShouldSerializeMessage()
        {
            return !string.IsNullOrEmpty(this.Message);
        }

        public ListDTO<T> Results
        {
            get;
            set;
        }

        public DTOListContainer(int page = 0, int total = 0)
        {
            this.Page = page;
            this.Total = total;
            this.Results = new ListDTO<T>();
        }

        public DTOListContainer(IList<T> range, int page = 0, int total = 0)
            : this(page, total)
        {
            this.Results.AddRange(range);
        }

        public void SetPageingInformation(int page, int total)
        {
            this.Page = page;
            this.Total = total;
        }
    }

    public class ListDTO<T> : List<T>
    {
        public ListDTO()
            : base()
        {

        }

        public ListDTO(IEnumerable<T> collection)
            : base(collection)
        {

        }

        public ListDTO(int capacity)
            : base(capacity)
        {

        }
    }
}
