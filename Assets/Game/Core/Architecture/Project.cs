using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Game.Core.Architecture.Services;
using Game.Core.Architecture.Services.Base;
using Game.Core.Utils;
using UnityEngine;

namespace Game.Core.Architecture
{
    public static class Project
    {
        public static ProjectContext ProjectContext
        {
            get
            {
                if (ProjectContext.IsCreated == false)
                {
                    _projectContext = ProjectContext.Create();
                    _projectContext.Initialize();
                }

                return _projectContext;
            }
        }

        public static bool IsInitialized { get; private set; }
        private static HashSet<BaseService> _container;
        private static ProjectContext _projectContext;

        public static async UniTask Initialize()
        {
            Debug.Log("Project Initializing...");
            _container = new HashSet<BaseService>();

            FillOrderedServices();
            FillRemainingServices();

            foreach (var s in _container)
            {
                Debug.Log("Initializing: " + s.GetType().Name);
                await s.Initialize();
                Debug.Log("Initialized: " + s.GetType().Name);
            }

            foreach (var s in _container)
            {
                s.PostInitialize();
            }

            _projectContext = ProjectContext.Create();
            _projectContext.Initialize();


            foreach (var s in _container)
            {
                s.OnProjectContextCreated(_projectContext);
            }

            IsInitialized = true;
            Debug.Log("Project Initialized!");
        }

        private static void FillRemainingServices()
        {
            var subs = ReflectionUtil.FindAllSubslasses<BaseService>();
            foreach (var subclass in subs)
            {
                if (_container.Any(s => s.GetType() == subclass))
                {
                    continue;
                }

                _container.Add(Activator.CreateInstance(subclass) as BaseService);
            }
        }

        private static void FillOrderedServices()
        {
            _container.Add(new LoadService());
            _container.Add(new SaveService());
        }

        public static async UniTask<T> Get<T>() where T : BaseService
        {
            if (!IsInitialized)
            {
                await UniTask.WaitUntil(() => IsInitialized);
            }
            if (_container == null)
            {
                throw new Exception("Firstly Initialize Project");
            }

            var instance = _container.FirstOrDefault(c => c is T) as T;
            return instance;
        }

        public static List<T> GetAll<T>() where T : class
        {
            if (_container == null)
            {
                throw new Exception("Firstly Initialize Project");
            }

            var f = _container.OfType<T>().ToList();
            return f;
        }

        public static void ActionForAll<T>(Action<T> action) where T : class
        {
            if (_container == null)
            {
                throw new Exception("Firstly Initialize Project");
            }

            var f = GetAll<T>();
            if (f.Count == 0)
            {
                return;
            }

            foreach (var p in f)
            {
                action?.Invoke(p);
            }
        }
    }
}