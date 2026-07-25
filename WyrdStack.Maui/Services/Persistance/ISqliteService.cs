using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace WyrdStack.Maui.Services.Persistance
{
	public interface ISqliteService
	{
		public Task<T> GetAllAsync<T>();
		public Task<T> GetAsync<T>(string id);
		public Task<T> CreateAsync<T>(T original) where T : class;	
		public Task<T> UpdateAsync<T>(T original) where T : class;
		public Task<int> DeleteAsync<T>(string id);
	}
}
