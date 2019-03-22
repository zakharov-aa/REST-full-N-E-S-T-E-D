/// Creates an async function which encodes an input into an HTTP request and returns a Success string response on OKs and Failure elsewise.
let sendRaw (http:AsyncArrow<HttpReq, HttpRes>) (methodKeyQueryData:'a -> HttpMethod * ConsulKey * (string * string) list * ConsulValue) : AsyncArrow<'a, Choicecstring, |> 
  http
   |> AsyncArrow.mapIn (fun a -> 
      let m,path,query,data = methodKeyQueryData a
      //Log.trace "method=%A path=%s query=%A data=%s” m path query data 
    let req =
      HttpReq.create ()
      |> HttpReq.withPathAndQuery path query 
      |> HttpReq.withMethod m
     if isNotNull data then req |> HttpReq.withBodyString data 
      else req)
     |> AsyncArrow.mapOutAsync (
        HttpRes.okBodyToString 
        |> AsyncArrow.strength 
        |> AsyncArrow.mapOut (fun (res,str) ->
          //Log.trace "response status=%0 body=%A" res.StatusCode str 
          Option.successor res.StatusCode str
)
