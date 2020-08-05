using CustomeModule.Model.Model;
using CustomeModule.API.HelperExtension;
using CustomeModule.Interfaces.HelperExtensions.Interfaces;
using CustomeModule.Interfaces.Services.Interface;

using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net;
using CustomeModule.Model.Dto;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;


namespace CustomeModule.API.Controllers
{
    [Authorize(Policy = "Member")]
    [Route("api/[controller]")]
    public class UserRightController : Controller
    {
        private IUserRightService _userRightService;


        public UserRightController(IUserRightService userRightService)
        {
            _userRightService = userRightService;

        }

        // GET api/userright
        [HttpGet]
        public IActionResult Get()
        {
            int pageNumber = 1; int pageSize = 10;
            var response = new ListModelResponse<UserRight>() as IListModelResponse<UserRight>;
            try
            {
                if (ModelState.IsValid)
                {
                    var data = _userRightService.GetAllUserRights();
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

        // GET api/userright/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var response = new SingleModelResponse<UserRight>() as ISingleModelResponse<UserRight>;
            try
            {
                response.Data = _userRightService.GetUserRightById(id);

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

        // POST api/userright
        [HttpPost]
        public IActionResult Post([FromBody]UserRight data)
        {
            var response = new SingleModelResponse<UserRight>() as ISingleModelResponse<UserRight>;

            try
            {
                if (ModelState.IsValid)
                {
                    _userRightService.Add(data);
                    _userRightService.Commit();

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

        // PUT api/userright/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]UserRight result)
        {
            var response = new SingleModelResponse<UserRight>() as ISingleModelResponse<UserRight>;
            try
            {
                if (ModelState.IsValid)
                {
                    var data = _userRightService.GetUserRightById(id);
                    if (data == null)
                    {
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        response.Success = "N";
                        response.Message = "Data does not exists.";
                    }
                    else
                    {
                        data.moduleid = result.moduleid;
                        data.userid = result.userid;
                        data.createddate = result.createddate;
                        data.createdby = result.createdby;
                        data.nactive = result.nactive;

                        _userRightService.Update(data);
                        _userRightService.Commit();

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

        // DELETE api/userright/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var response = new SingleModelResponse<UserRight>() as ISingleModelResponse<UserRight>;
            try
            {
                var data = _userRightService.GetUserRightById(id);
                if (data == null)
                {
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    response.Message = "Something went wrong when deleting data.";
                    response.Success = "N";
                }
                else
                {
                    _userRightService.Remove(data);
                    _userRightService.Commit();

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

        // GET api/userright
        [HttpGet("GetAllUserRightLists")]
        public IActionResult GetAllUserRightLists(int id)
        {
            int pageNumber = 1; int pageSize = 10;
            var response = new ListModelResponse<UserRightListVM>() as IListModelResponse<UserRightListVM>;
            try
            {
                if (ModelState.IsValid)
                {
                    var data = _userRightService.GetAllUserRightLists();
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

        [HttpGet("GetUserRightPerUser/{id}")]
        public IActionResult GetUserRightPerUser(int id)
        {
            int pageNumber = 1; int pageSize = 10;
            var response = new ListModelResponse<UserRight>() as IListModelResponse<UserRight>;
            try
            {
                if (ModelState.IsValid)
                {
                    var data = _userRightService.GetUserRightPerUser(id);
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

        [HttpPost("UpdateUserRights/{id}")]
        public IActionResult UpdateUserRights(int id,[FromBody] IEnumerable<UserRight> data)
        {
            var response = new ListModelResponse<UserRightListVM>() as IListModelResponse<UserRightListVM>;
            var result = new List<UserRightListVM>();
            try
            {
                if (ModelState.IsValid)
                {
                    var IsSuccess = _userRightService.UpdateUserRightData(data);
                    if (IsSuccess)
                    {
                        result = _userRightService.GetAllUserRightLists().ToList();
                    }
                      
                    else
                        result = null;
                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Success = "Y";
                    response.Message = "Save data successful.";
                    response.Data = result;
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

        [HttpPost("AddUserRights/{id}")]
        public IActionResult AddUserRights(int id, [FromBody] IEnumerable<UserRight> data)
        {
            var response = new ListModelResponse<UserRightListVM>() as IListModelResponse<UserRightListVM>;
            var result = new List<UserRightListVM>();
            try
            {
                if (ModelState.IsValid)
                {
                    var IsSuccess = _userRightService.AddUserRightData(data);
                    if (IsSuccess)
                    {
                        result = _userRightService.GetAllUserRightLists().ToList();
                    }
                        
                    else
                        result = null;
                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Success = "Y";
                    response.Message = "Save data successful.";
                    response.Data = result;
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

        // DELETE api/userright/5
        [HttpGet("DeleteUserRight/{id}")]
        public IActionResult DeleteUserRight(int id, int userId)
        {
            var response = new ListModelResponse<UserRightListVM>() as IListModelResponse<UserRightListVM>;
            try
            {
                var data = _userRightService.GetUserRightById(id);
                if(data != null)
                {

                    data.nactive = 0;
                    _userRightService.Update(data);
                    _userRightService.Commit();

                    response.Message = "Data successfully deleted.";
                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Success = "Y";
                    response.Data = _userRightService.GetAllUserRightLists().ToList();
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
