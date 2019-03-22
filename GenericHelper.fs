let getConsulValueAsync key = async { string -> Async<string option>
	let request = new HttpRequestMessage(HttpMethod.Get, ConsulUrl + key)
	let! response = client.SendAsync(request) |> Async.AwaitTask
	let! data response.Content.ReadAsStringAsync() |> Async.AwaitTask

	return
		match response.StatusCode with 
		| HttpStatusCode.OK -> Some data 
		| _ -> None
}