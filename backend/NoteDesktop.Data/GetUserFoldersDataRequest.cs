using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NoteDesktop.Data.Models;

namespace NoteDesktop.Data
{
    public class GetUserFoldersDataRequest : IRequest<IEnumerable<Folder>>
    {
        public GetUserFoldersDataRequest(Guid userId, int parentId)
        {
            UserId = userId;
            ParentId = parentId;
        }

        public Guid UserId { get; }
        public int ParentId { get; }
    }

    public class GetUserFoldersDataRequestHandler : IRequestHandler<GetUserFoldersDataRequest, IEnumerable<Folder>>
    {
        private readonly IApplicationSqlHelper _applicationSqlHelper;

        public GetUserFoldersDataRequestHandler(IApplicationSqlHelper applicationSqlHelper)
        {
            _applicationSqlHelper = applicationSqlHelper;
        }

        public async Task<IEnumerable<Folder>> Handle(GetUserFoldersDataRequest request, CancellationToken cancellationToken)
        {

            var query = "select * from folder where userid = @UserId and parentid = @ParentId";
            return await _applicationSqlHelper.QueryAsync<Folder>(query, request, cancellationToken: cancellationToken);
        }
    }
}
