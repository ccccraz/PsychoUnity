using System;
using System.Net;
using System.Threading.Tasks;
using PsychoUnity.Manager;
using UnityEngine;

namespace PsychoUnity
{
    public class MiniHttpServer : Singleton<MiniHttpServer>
    {
        private HttpListener _listener;
        private bool _isRunning;
        
        public void Create(string[] prefixes)
        {
            _listener = new HttpListener();
            
            if (prefixes == null || prefixes.Length == 0)
                throw new ArgumentException("prefixes");
            
            foreach (var variable in prefixes)
            {
                _listener.Prefixes.Add(variable);
            }
        }

        public async Task StartAsync()
        {
            _listener.Start();
            _isRunning = true;
            
            while (_isRunning)
            {
                try
                {
                    var context = await _listener.GetContextAsync();
                    ProcessRequest(context);
                }
                catch (Exception e)
                {
                    Debug.LogWarning(e);
                    throw;
                }
            }
        }
        
        private static void ProcessRequest(HttpListenerContext context)
        {
            var request = context.Request;
            var url = request.Url.AbsolutePath;
            
            EventManager.Instance.EventTrigger(url, context);
        }

        public void Close()
        {
            _isRunning = false;
            _listener.Abort();
        }
    }
}