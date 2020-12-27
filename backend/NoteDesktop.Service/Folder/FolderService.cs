using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoteDesktop.Data;

namespace NoteDesktop.Service.Folder
{
    public class FolderService : IFolderService
    {
        private readonly IMediator _mediator;

        public FolderService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<List<Data.Models.Folder>> GetUserFoldersAsync(Guid userId, int parentId)
        {
            var response = await _mediator.Send(new GetUserFoldersDataRequest(userId, parentId));
            return response.ToList();
        }
    }
}
