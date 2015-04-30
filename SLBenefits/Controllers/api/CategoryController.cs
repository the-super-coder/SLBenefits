using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SLBenefits.Core.Domain;
using SLBenefits.Core.Service;

namespace SLBenefits.Controllers.api
{
    public class CategoryController : ApiController
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost]
        [Route("api/category/save")]
        public IHttpActionResult Save(Category postModel)
        {
            var id = _categoryService.Save(postModel);
            return this.Ok(id);
        }

        [HttpGet]
        [Route("api/category/getall")]
        public IHttpActionResult GetAll()
        {
            var results = _categoryService.GetAll();
            return this.Ok(results);
        }
    }
}
