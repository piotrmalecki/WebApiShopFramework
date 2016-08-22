using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using WebApiMGR.Domain;
using WebApiMGR.DTOs.Container;

namespace WebApiMGR.Controllers.BaseController
{

        public abstract class BaseApiController : ApiController
        {
            protected ShopFrameworkDomain shopFrameworkDomain;

            /// <summary>
            /// Returns controller route prefix
            /// </summary>
            protected string controllerRoutePrefix
            {
                get
                {
                    // Get prefix via reflection:
                    RoutePrefixAttribute prefixAttr = ((RoutePrefixAttribute)this.GetType().GetCustomAttributes(true).Where(x => x.GetType() == typeof(RoutePrefixAttribute)).FirstOrDefault());

                    if (prefixAttr != null)
                        return prefixAttr.Prefix;
                    else
                        return "";
                }
            }

            /// <summary>
            /// Returns controller route uri
            /// </summary>
            protected Uri controllerRouteUri
            {
                get
                {
                    if (this.Request != null && this.Request.RequestUri != null)
                    {
                        return new Uri(this.Request.RequestUri.Scheme + "://" + this.Request.RequestUri.Authority + "/" + this.controllerRoutePrefix);
                    }

                    return null;
                }
            }

            /// <summary>
            /// Default constructor
            /// </summary>
            public BaseApiController()
            {
                this.shopFrameworkDomain = new ShopFrameworkDomain();
            }

            /// <summary>
            /// Creates log inside of database
            /// </summary>
            /// <param name="level">Log level</param>
            /// <param name="message">Logged message</param>
            /// <param name="ex">Optional exception to log</param>
            //protected void Log(LogLevel level, string message, Exception ex = null)
            //{
            //    Log4NetLogs.LogWebApi(level, message, this.GetType(), ex, this.Request);
            //}

            /// <summary>
            //  Creates a DTO collection wrapper arround System.Web.Http.Results.OkNegotiatedContentResult<T> for collection results
            /// </summary>
            /// <typeparam name="T">Collection element type</typeparam>
            /// <param name="collection">Wrapped collection</param>
            /// <param name="page">Page of paged results</param>
            /// <param name="pageSize">Page size</param>
            /// <param name="message">Additional message</param>
            /// <returns></returns>
            protected IHttpActionResult OkCollectionResult<T>(IList<T> collection, string message = "", int pageSize = 0, int page = -1)
            {
                // Page results if needed:
                int totalAmountOfResults = 0;
                if ((pageSize > 0) && (page >= 0))
                {
                    totalAmountOfResults = collection.Count;
                    collection = collection.Skip(page * pageSize).Take(pageSize).ToList();
                }

                DTOListContainer<T> container = new DTOListContainer<T>(collection, page, totalAmountOfResults);
                container.Message = message;
                return Ok(container);
            }

            /// <summary>
            /// Creates no content OK result (204 code).
            /// Use when response body is intentionally empty
            /// </summary>
            /// <returns>Http status code - 204</returns>
            protected IHttpActionResult NoContent()
            {
                return StatusCode(HttpStatusCode.NoContent);
            }

            /// <summary>
            /// Creates Accepted result (202 code).
            /// Use to indicate start of asynchronous action.
            /// </summary>
            /// <returns>Http status code - 202</returns>
            protected IHttpActionResult Accepted()
            {
                return StatusCode(HttpStatusCode.Accepted);
            }

            /// <summary>
            /// Creates Created result (201 code).
            /// Use to indicate successful resource creation without 
            /// returning the resource content or new resource url.
            /// </summary>
            /// <returns>Http status code - 202</returns>
            protected IHttpActionResult Created()
            {
                return StatusCode(HttpStatusCode.Created);
            }
        }
    }
