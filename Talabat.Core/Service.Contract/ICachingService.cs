﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Service.Contract
{
	public interface ICachingService
	{
		Task CacheResponseAsync(string key,object response,TimeSpan timeToLive);
		Task<string?> GetCashedResponseAsync(string key);
	}
}
