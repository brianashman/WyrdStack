using System;
using System.Collections.Generic;
using System.Text;

namespace WyrdStack.Maui.Services.Persistance
{
	public class TaskService : ISqliteService
	{
		public Task<T> CreateAsync<T>(T original) where T : class
		{
			throw new NotImplementedException();
		}

		public Task<int> DeleteAsync<T>(string id)
		{
			throw new NotImplementedException();
		}

		public Task<T> GetAllAsync<T>()
		{
			throw new NotImplementedException();
		}

		public Task<T> GetAsync<T>(string id)
		{
			throw new NotImplementedException();
		}

		public Task<T> UpdateAsync<T>(T original) where T : class
		{
			throw new NotImplementedException();
		}
	}
}
