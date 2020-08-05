using CustomeModule.Model.Model;
using CustomeModule.API.HelperExtension;
using CustomeModule.Interfaces.HelperExtensions.Interfaces;
using CustomeModule.Interfaces.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net;
using CustomeModule.Model.Dto;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Authorization;

using CustomeModule.Interfaces.MailHelper.Interfaces;

namespace CustomeModule.API.Controllers
{
    [Authorize(Policy = "Member")]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private IUserService _userService;
        private IGlobalServices _globalService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private IMailHandler _handler;


        public UserController(IUserService userService, IGlobalServices globalService, 
            IHostingEnvironment hostingEnvironment, IMailHandler handler
            )
        {
            _userService = userService;
            _globalService = globalService;
            _hostingEnvironment = hostingEnvironment;
            _handler = handler;
           
        }

        // GET api/user
        [HttpGet]
        public IActionResult Get()
        {
            int pageNumber = 1; int pageSize = 10;
            var response = new ListModelResponse<User>() as IListModelResponse<User>;
            try
            {
                if (ModelState.IsValid)
                {
                    var data = _userService.GetAllUsers();
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


        // GET api/user/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var response = new SingleModelResponse<User>() as ISingleModelResponse<User>;
            try
            {
                response.Data = _userService.GetUserById(id);

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

        // POST api/user
        [HttpPost]
        public IActionResult Post([FromBody]User data)
        {
            var response = new SingleModelResponse<User>() as ISingleModelResponse<User>;

            try
            {
                if (ModelState.IsValid)
                {
                    _userService.Add(data);
                    _userService.Commit();

                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Success = "Y";
                    response.Message = "Save data successful.";
                    response.Data = data;
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

        // PUT api/user
        [HttpPut]
        public IActionResult Put([FromBody]User data, int id)
        {
            var response = new SingleModelResponse<User>() as ISingleModelResponse<User>;
            try
            {
                if (ModelState.IsValid)
                {
                    var details = _userService.GetUserById(id);
                    if (details == null)
                    {
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        response.Success = "N";
                        response.Message = "Data does not exists.";
                    }
                    else
                    {
                        details.branchid = data.branchid;
                        details.name = data.name;
                        details.email = data.email;
                        details.telno = data.telno;
                        details.address = data.address;
                        details.username = data.username;
                        details.password = data.password;                        
                        details.createddate = data.createddate;
                        details.createdby = data.createdby;
                        details.lastupdateddate = data.lastupdateddate;
                        details.lastupdatedby = data.lastupdatedby;
                        details.nactive = data.nactive;

                        _userService.Update(details);
                        _userService.Commit();

                        response.StatusCode = (int)HttpStatusCode.OK;
                        response.Success = "Y";
                        response.Message = "Data updated successfully.";
                        response.Data = details;
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

        // DELETE api/user/5
        [HttpGet("DeleteUser/{id}")]
        public IActionResult DeleteUser(int id, int userId)
        {
            var response = new ListModelResponse<UserListVM>() as IListModelResponse<UserListVM>;
            try
            {
                var data = _userService.GetUserById(id);
                if (data == null)
                {
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    response.Message = "Something went wrong when deleting data.";
                    response.Success = "N";
                }
                else
                {


                    data.nactive = 0;
                    _userService.Update(data);
                    _userService.Commit();

                    response.Message = "Data successfully deleted.";
                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Success = "Y";
                    response.Data = _userService.GetAllUserLists();
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

        // GET api/user
      
        [HttpGet("GetAllUsers")]
        public IActionResult GetAllUserLists()
        {
            int pageNumber = 1; int pageSize = 10;
            var response = new ListModelResponse<UserListVM>() as IListModelResponse<UserListVM>;
            try
            {
                if (ModelState.IsValid)
                {
                    var data = _userService.GetAllUserLists();
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

        // POST api/user/postuserdata
        [HttpPost("PostUserData")]
        public IActionResult PostUserData([FromBody]User user)
        {
            var response = new SingleModelResponse<UserListVM>() as ISingleModelResponse<UserListVM>;
            var result = new UserListVM();
            string error = "N";
            try
            {
                if (ModelState.IsValid)
                {
                    user.signaturepath = "";
                    var addedUser =_userService.AddUserData(user);




                    if (addedUser.userid != 0)
                    {
                        result = _userService.GetUserListsById(addedUser.userid);
                        error = "E";
                        SendEmail(user.email, user.name, user);
                    
                    }
                    else
                    {
                        result = null;
                    }
                        
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
                response.Success = error;
                response.Message = ex.Message;
                if (error == "E")
                {
                    response.Data = result;
                }
             
            }
            return response.ToHttpResponse();
        }

        // PUT api/user/putuserdata
        [HttpPost("PutUserData")]
        public IActionResult PutUserData([FromBody]User user)
        {
            var response = new SingleModelResponse<UserListVM>() as ISingleModelResponse<UserListVM>;
            var result = new UserListVM();
            try
            {
                if (ModelState.IsValid)
                {
                    var details = _userService.GetUserById(user.userid);
                    if(details == null)
                    {
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        response.Success = "N";
                        response.Message = "Data does not exists.";
                    }
                    else
                    {
                        details.branchid = user.branchid;
                        details.name = user.name;
                        details.email = user.email;
                        details.telno = user.telno;
                        details.address = user.address;
                        details.username = user.username;
                        details.password = user.password;                        
                        details.lastupdateddate = user.lastupdateddate;
                        details.lastupdatedby = user.lastupdatedby;
                        details.nactive = user.nactive;
                        details.usertypeid = user.usertypeid;

                        var IsSuccess = _userService.UpdateUserData(details);                        
                        if (IsSuccess)
                        {              
                            result = _userService.GetUserListsById(details.userid);
                        }

                        else
                        {
                            result = null;
                        }
                            
                        response.StatusCode = (int)HttpStatusCode.OK;
                        response.Success = "Y";
                        response.Message = "Save data successful.";
                        response.Data = result;
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

        [HttpGet("GetDate")]
        public IActionResult GetDate()
        {
         
            var response = new SingleModelResponse<DateTime>() as ISingleModelResponse<DateTime>;
            try
            {
                if (ModelState.IsValid)
                {
                    var data = _globalService.GetCurrentDateTimeInCurrentTimeZone();                                                          
                    response.StatusCode = (int)HttpStatusCode.OK;                    
                    response.Success = "Y";                    
                    response.Data = data;
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

        // GET api/user/userdata
        [HttpGet("GetUserData")]
        public IActionResult GetUserData(string email, string password)
        {
            var response = new SingleModelResponse<User>() as ISingleModelResponse<User>;
            try
            {
                response.Data = _userService.GetAllUsers().Where(x => x.email == email && x.password == password).FirstOrDefault();

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

        [HttpPost("UploadImage/{id}")]
        public IActionResult UploadImage(int id)
        {
            var response = new SingleModelResponse<User>() as ISingleModelResponse<User>;

            try
            {
                var file11 = Request.Form.Files;
                string physicalWebRootPath = _hostingEnvironment.WebRootPath.ToString() + "\\UserProfile\\";
                string fileSave = string.Empty;
                foreach (var file in file11)
                {
                    if (!IsValidImageFile(file.ContentType))
                    {
                        return Json(new { errMsg = "" });
                    }

                    if (file.Length > 0)
                    {
                        string forupload = physicalWebRootPath + "User_" + id + Path.GetExtension(file.FileName);
                        fileSave = Url.Content("~/UserProfile/User_" + id + Path.GetExtension(file.FileName));
                        if (System.IO.File.Exists(forupload))
                        { System.IO.File.Delete(forupload); }

                        using (var fileStream = new FileStream(Path.Combine(physicalWebRootPath, "User_" + id + Path.GetExtension(file.FileName)), FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                            _userService.updateImagePath(id, fileSave);
                            _userService.Commit();
                        }
                    }
                }

                response.StatusCode = (int)HttpStatusCode.OK;
                response.Success = "Y";
                response.Message = fileSave;

            }
            catch (Exception ex)
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Message = ex.Message;
                response.Success = "N";
            }

            return response.ToHttpResponse();
        }

        [HttpPost("UploadSignaure/{id}")]
        public IActionResult UploadSignaure(int id)
        {
            var response = new SingleModelResponse<User>() as ISingleModelResponse<User>;

            try
            {
                var file11 = Request.Form.Files;
                string physicalWebRootPath = _hostingEnvironment.WebRootPath.ToString() + "\\Signature\\";
                string fileSave = string.Empty;
                foreach (var file in file11)
                {
                    if (!IsValidImageFile(file.ContentType))
                    {
                        return Json(new { errMsg = "" });
                    }

                    if (file.Length > 0)
                    {
                        string forupload = physicalWebRootPath + "Signature_" + id + Path.GetExtension(file.FileName);
                        fileSave = Url.Content("~/Signature/Signature_" + id + Path.GetExtension(file.FileName));
                        if (System.IO.File.Exists(forupload))
                        { System.IO.File.Delete(forupload); }

                        using (var fileStream = new FileStream(Path.Combine(physicalWebRootPath, "Signature_" + id + Path.GetExtension(file.FileName)), FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                            _userService.updateSignature(id, fileSave);
                            _userService.Commit();
                        }
                    }
                }

                response.StatusCode = (int)HttpStatusCode.OK;
                response.Success = "Y";
                response.Message = fileSave;

            }
            catch (Exception ex)
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Message = ex.Message;
                response.Success = "N";
            }

            return response.ToHttpResponse();
        }

        private bool IsValidImageFile(string contentType)
        {
            return contentType.Contains("image");
        }


        [HttpGet("ChangePassword/{id}")]
        public IActionResult ChangePassword(int id,string oldPassword, string newPassword)
        {            
            var response = new SingleModelResponse<Boolean>() as ISingleModelResponse<Boolean>;
            try
            {
                if (ModelState.IsValid)
                {
                    var data = _userService.changePassword(id, oldPassword, newPassword);
                    if (data)
                    {
                        var newUser = _userService.GetUserById(id);
                        newUser.password = newPassword;
                        _userService.Update(newUser);
                        _userService.Commit();
                    }                    
                    response.StatusCode = (int)HttpStatusCode.OK;                    
                    response.Success = "Y";
                    response.Data = data;
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Message = ex.Message;                
                response.Success = "N";
                response.Data = false;
            }
            return response.ToHttpResponse();
        }

        private void SendEmail(string sendTo, string receiver, User data)
        {           
            _handler.SendMail("noreply@allbank.com.ph", sendTo, "", "", data.name, data, 1, "");

        }
    }
}
