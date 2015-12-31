﻿using AutoMerger.Shared.Ninject;
using BranchManager.Core.Ninject;
using Ninject;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Dependencies;

namespace BranchManager
{
	public class NinjectHttpResolver : IDependencyResolver, IDependencyScope
	{
		public IKernel Kernel { get; private set; }

		public NinjectHttpResolver(params NinjectModule[] modules)
		{
			Kernel = new StandardKernel(modules);
		}

		public NinjectHttpResolver(Assembly assembly)
		{
			Kernel = new StandardKernel();
			Kernel.Load(assembly);
		}

		public object GetService(Type serviceType)
		{
			return Kernel.TryGet(serviceType);
		}

		public IEnumerable<object> GetServices(Type serviceType)
		{
			return Kernel.GetAll(serviceType);
		}

		public void Dispose()
		{
		}

		public IDependencyScope BeginScope()
		{
			return this;
		}
	}

	public class NinjectHttpModules
	{
		public static NinjectModule[] Modules
		{
			get { return new NinjectModule[] { new CoreModule(), new SharedModule(new string[0]) }; }
		}
	}

	public class NinjectHttpContainer
	{
		private static NinjectHttpResolver _resolver;

		public static void RegisterModules(NinjectModule[] modules)
		{
			_resolver = new NinjectHttpResolver(modules);
			GlobalConfiguration.Configuration.DependencyResolver = _resolver;
		}

		public static void RegisterAssembly()
		{
			_resolver = new NinjectHttpResolver(Assembly.GetExecutingAssembly());
			GlobalConfiguration.Configuration.DependencyResolver = _resolver;
		}

		public static T Resolve<T>()
		{
			return _resolver.Kernel.Get<T>();
		}
	}
}