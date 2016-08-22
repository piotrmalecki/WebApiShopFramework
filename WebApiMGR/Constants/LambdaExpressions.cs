using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using WebApiMGR.DTOs;
using WebApiMGR.Data;
using System.Web;
using WebApiMGR.Data.Models.Generated;

namespace WebApiMGR.Constants
{
    public class LambdaExpressions
    {
        public static Expression<Func<Item, object>>[] ItemNavigationProperties = {x => x.Catagory.Name };
    }
}