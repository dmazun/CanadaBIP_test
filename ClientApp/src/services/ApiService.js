export class ApiService {

  sendRequest(url, method = "GET", data = {}) {
    if (method === "GET") {
      return fetch(url).then((result) =>
        result.json().then((data) => {
          if (result.ok) return data;
          throw data.Message;
        })
      );
    }

    return fetch(url, {
      method,
      body: data,
      headers: { "Content-Type": "application/json;charset=UTF-8" },
    }).then((result) => {
      if (result.ok) {
        console.log('RR', result)
        return result.text().then((text) => text && JSON.parse(text));
      }
      return result.json().then((json) => {
        throw json.Message;
      });
    });
  }
}
