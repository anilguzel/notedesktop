using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NoteDesktop.Api.Helpers;
using NoteDesktop.Service.Folder;

namespace NoteDesktop.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[Authorize]
    public class FolderController : AuthorizedControllerBase
    {
        private readonly IFolderService _folderService;

        public FolderController(IFolderService folderService)
        {
            _folderService = folderService;
        }

        [HttpGet("{parentId}")]
        public async Task<IActionResult> GetAsync(int parentId)
        {
            //var response = await _folderService.GetUserFoldersAsync(GetUserId(), parentId);
            var response = await _folderService.GetUserFoldersAsync(new Guid("6b63ce59-9f07-4c84-9ccc-f943affe19ad"), parentId);
            return Ok(response);
        }
    }
}
