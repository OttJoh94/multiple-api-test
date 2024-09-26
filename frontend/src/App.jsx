import { useEffect, useState } from "react";
import "./App.css";

function App() {
  const [promptMessage, setPromptMessage] = useState("");
  const [apiResponse, setApiResponse] = useState("Här kommer svaret vara");
  const [isPromptRecieved, setIsPromptRecieved] = useState(false);
  const [urlMessage, setUrlMessage] = useState("Loading url");
  const [urlRecieved, setUrlRecieved] = useState(false);
  const [prompt, setPrompt] = useState({});
  const [count, setCount] = useState(0);

  function postPrompt() {
    const postOptions = {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({ prompt: promptMessage }),
    };

    fetch("https://localhost:7194/api/Prompt", postOptions)
      .then((res) => res.json())
      .then((data) => printMessage(data));
  }

  function printMessage(prompt) {
    setPrompt(prompt);
    setApiResponse(`Prompt: ${prompt.prompt}   -   Id: ${prompt.id}`);
    setIsPromptRecieved(true);
  }

  useEffect(() => {
    if (isPromptRecieved) {
      const interval = setInterval(async () => {
        try {
          const response = await fetch(
            `https://localhost:7194/api/PromptUrl/${prompt.id}`
          );
          if (response.status === 200) {
            const result = await response.json();
            setUrlMessage(result.url);
            setUrlRecieved(true);
            setIsPromptRecieved(false);
          }
        } catch (err) {
          console.log(err);
        }
      }, 1000);

      return () => clearInterval(interval);
    }
  }, [isPromptRecieved]);

  return (
    <>
      <input
        type="text"
        value={promptMessage}
        onChange={(e) => setPromptMessage(e.target.value)}
      />
      <button onClick={postPrompt}>Skicka prompt</button>
      <br />
      <br />
      <h3>{apiResponse}</h3>
      <br />
      <br />
      <h3>{urlMessage}</h3>
      <button onClick={() => setCount(count + 1)}>{count}</button>
    </>
  );
}

export default App;
