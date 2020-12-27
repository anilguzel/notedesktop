using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NoteDesktop.Service.Folder
{
    public interface IFolderService
    {
        Task<List<Data.Models.Folder>> GetUserFoldersAsync(Guid userId, int parentId);
    }
}
