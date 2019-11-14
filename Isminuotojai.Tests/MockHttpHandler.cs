using Isminuotojai.Resources;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace Isminuotojai.Tests
{
    class MockHttpHandler : HttpMessageHandler
    {
        private const string Site = "https://localhost:44397";
        const string apipath = "/api/player/";
        const string mediaType = "application/json";
        private static readonly string requestUri = Site + apipath;
        Object obj;
        HttpStatusCode statuscode = HttpStatusCode.OK;


        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        public void SetReturnObj(Object obj)
        {
            this.obj = obj;
        }

        public void SetHttpStatus(HttpStatusCode code)
        {
            statuscode = code;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            //if (request.Method.Equals(HttpMethod.Post))
            //{
            //    if (request.RequestUri.Equals(requestUri))
            //    {
            //        var result = JsonConvert.DeserializeObject<PlayerData>(await request.Content.ReadAsStringAsync());
            //        return new HttpResponseMessage()
            //        {
            //            StatusCode = HttpStatusCode.OK,
            //            Content = new StringContent(""),
            //        };
            //    }else if (request.RequestUri.Equals(requestUri + "DoMove/"))
            //    {
            //        var move = JsonConvert.DeserializeObject<Move>(await request.Content.ReadAsStringAsync());
            //        var map = new char[10, 10];
            //        for(int i = 0; i < 10; i++)
            //        {
            //            for(int j = 0; j < 10; j++)
            //            {
            //                map[i, j] = 'u';
            //            }
            //        }
            //        switch (move.Type)
            //        {
            //            case MoveType.Mark:
            //                map[move.X, move.Y] = 'm';
            //                break;
            //            case MoveType.Reveal:
            //                map[move.X, move.Y] = '0';
            //                break;
            //            case MoveType.Set:
            //                map[move.X, move.Y] = 't';
            //                break;
            //            case MoveType.Unset:
            //                map[move.X, move.Y] = 'u';
            //                break;
            //        }
            //        var result = new MineResult { status = GameStatus.Ongoing, map = map, success = true, turn = true };
            //        return new HttpResponseMessage()
            //        {
            //            StatusCode = HttpStatusCode.OK,
            //            Content = new StringContent(JsonConvert.SerializeObject(result)),
            //        };
            //    }else if (request.RequestUri.Equals(requestUri + "DoMove/"))
            //    {

            //    }
            //}
            //else if (request.Method.Equals(HttpMethod.Get))
            //{

            //}
            //else if (request.Method.Equals(HttpMethod.Put))
            //{

            //}

            //    request = new HttpRequestMessage
            //{
            //    Method = HttpMethod.Put,
            //    RequestUri = new Uri(requestUri),
            //    Content = new StringContent(stringPayload, Encoding.UTF8, mediaType),
            //};
            return new HttpResponseMessage()
            {
                StatusCode = statuscode,
                Content = new StringContent(JsonConvert.SerializeObject(obj)),
            };
        }
    }
}
