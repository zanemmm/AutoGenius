using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Windows;
using System.Threading;

namespace AutoGenius
{
    class HTTPServer
    {
        public bool serverStart = false;

        HttpListener httpListener;
        Thread handleRequestThread;
        ThreadStart handleRequestThreadStart;

        public bool Start(string port)
        {
            try
            {
                httpListener = new HttpListener();
                httpListener.AuthenticationSchemes = AuthenticationSchemes.Anonymous;
                httpListener.Prefixes.Add(string.Format("http://127.0.0.1:{0}/", port));
                httpListener.Start();
                // 创建并启动 HTTP 线程
                handleRequestThreadStart = new ThreadStart(HandleRequest);
                handleRequestThread = new Thread(handleRequestThreadStart);
                handleRequestThread.Start();
                serverStart = true;
                Log.Info("HTTP 服务已启动,端口: " + port);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "HTTP 服务启动失败!");
                Log.Info("HTTP 服务启动失败");
            }
            return serverStart;
        }

        public bool Stop()
        {
            try
            {
                handleRequestThread.Abort();
                httpListener.Stop();
                serverStart = false;
                Log.Info("HTTP 服务已停止");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "HTTP 服务停止失败!");
                Log.Info("HTTP 服务停止失败");
            }
            return !serverStart;
        }

        private void HandleRequest()
        {
            while (true) 
            {
                HttpListenerContext context = httpListener.GetContext();
                Thread t = new Thread(() => {
                    var request = context.Request;
                    var querys = request.QueryString;
                    var type = querys.Get("type");
                    var response = new HTTPResponse(context.Response);
                    var ret = false;
                    switch (type)
                    {
                        case "mouse":
                            ret = Mouse.Proccess(querys, response);
                            break;
                        case "keyboard":
                            ret = Keyboard.Proccess(querys, response);
                            break;
                        case "display":
                            ret = Display.Proccess(querys, response);
                            break;
                    }
                    if (!ret) 
                    {
                        response.NotFoundResponse();
                    }
                    System.GC.Collect();
                });
                t.Start();
            }
        }
    }
}
