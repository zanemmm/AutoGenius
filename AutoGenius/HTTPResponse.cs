using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace AutoGenius
{
    class HTTPResponse
    {
        public HttpListenerResponse response;

        public HTTPResponse(HttpListenerResponse inResponse)
        {
            response = inResponse;
        }

        public void DataResponse(string data)
        {
            response.StatusCode = 200;
            response.ContentType = "application/json;charset=gb2312";
            byte[] buffer = Encoding.Default.GetBytes(string.Format("{{\"code\":200, \"msg\": \"ok\", \"data\": {0}}}", data));
            response.OutputStream.Write(buffer, 0, buffer.Length);
            Close();
        }

        public void SuccessResponse()
        {
            DataResponse("null");
        }

        public void NotFoundResponse()
        {
            response.StatusCode = 404;
            response.ContentType = "application/json";
            byte[] buffer = Encoding.Default.GetBytes("{\"code\":404, \"msg\": \"fail\", \"data\": null}");
            response.OutputStream.Write(buffer, 0, buffer.Length);
            Close();
        }

        private void Close()
        {
            try
            {
                response.Close();
            }
            catch
            {

            }
        }
    }
}
