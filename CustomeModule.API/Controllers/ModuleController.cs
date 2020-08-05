using CustomeModule.Model.Model;
using CustomeModule.API.HelperExtension;
using CustomeModule.Interfaces.HelperExtensions.Interfaces;
using CustomeModule.Interfaces.Services.Interface;

using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Authorization;

namespace CustomeModule.API.Controllers
{
    [Authorize(Policy = "Member")]
    [Route("api/[controller]")]
    public class ModuleController : Controller
    {
        private IModuleService _moduleService;

        public ModuleController(IModuleService moduleService)
        {
            _moduleService = moduleService;
        }

        // GET api/module
        [HttpGet]
        public IActionResult Get()
        {
            int pageNumber = 1; int pageSize = 10;
            var response = new ListModelResponse<Module>() as IListModelResponse<Module>;
            try
            {
                if (ModelState.IsValid)
                {
                    var data = _moduleService.GetAllModules();
                    int skip = (pageNumber - 1) * pageSize;
                    int totalRecordCount = data.Count();
                    int pageCount = totalRecordCount > 0 ? (int)Math.Ceiling(totalRecordCount / (double)pageSize) : 0;

                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Count = data.Count();
                    response.Success = "Y";
                    response.PageNumber = pageNumber;
                    response.PageSize = pageSize;
                    response.Data = data.ToList();
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Message = ex.Message;
                response.Count = 0;
                response.Success = "N";
            }
            return response.ToHttpResponse();
        }

        // GET api/module/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var response = new SingleModelResponse<Module>() as ISingleModelResponse<Module>;
            try
            {
                response.Data = _moduleService.GetModuleById(id);

                response.StatusCode = (int)HttpStatusCode.OK;
                response.Success = "Y";

            }
            catch (Exception ex)
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Message = ex.Message;
                response.Success = "N";
            }
            return response.ToHttpResponse();
        }

        // POST api/module
        [HttpPost]
        public IActionResult Post([FromBody]Module result)
        {
            var response = new SingleModelResponse<Module>() as ISingleModelResponse<Module>;

            try
            {
                if (ModelState.IsValid)
                {
                    _moduleService.Add(result);
                    _moduleService.Commit();

                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Success = "Y";
                    response.Message = "Save data successful.";
                }
                else
                {
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    response.Success = "N";
                    response.Message = "Invalid entry.";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Success = "N";
                response.Message = ex.Message;
            }
            return response.ToHttpResponse();
        }

        // PUT api/module/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Module result)
        {
            var response = new SingleModelResponse<Module>() as ISingleModelResponse<Module>;
            try
            {
                if (ModelState.IsValid)
                {
                    var data = _moduleService.GetModuleById(id);
                    if (data == null)
                    {
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        response.Success = "N";
                        response.Message = "Data does not exists.";
                    }
                    else
                    {
                        data.modulename = result.modulename;

                        _moduleService.Update(data);
                        _moduleService.Commit();

                        response.StatusCode = (int)HttpStatusCode.OK;
                        response.Success = "Y";
                        response.Message = "Data updated successfully.";
                    }
                }
                else
                {
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    response.Success = "N";
                    response.Message = "Invalid entry.";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Success = "N";
                response.Message = ex.Message;
            }
            return response.ToHttpResponse();
        }

        // DELETE api/module/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var response = new SingleModelResponse<Module>() as ISingleModelResponse<Module>;
            try
            {
                var room = _moduleService.GetModuleById(id);
                if (room == null)
                {
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    response.Message = "Something went wrong when deleting data.";
                    response.Success = "N";
                }
                else
                {
                    _moduleService.Remove(room);
                    _moduleService.Commit();

                    response.Message = "Data successfully deleted.";
                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Success = "Y";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Message = ex.Message;
                response.Success = "N";
            }
            return response.ToHttpResponse();
        }
    }
}
