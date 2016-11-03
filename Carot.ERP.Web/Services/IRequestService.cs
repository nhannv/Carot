using System;

namespace Carot.ERP.Web.Services
{
    public interface IRequestService
	{
		void AddObjectToDispose(IDisposable obj);
    }
}