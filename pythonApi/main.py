from flask import Flask, request, jsonify
import requests
import threading
import time

app = Flask(__name__)


@app.route("/generate-prompt", methods=["POST"])
def create_image():
    try:
        data = request.get_json()
        if not data or "Prompt" not in data:
            return jsonify({"error": "Invalid input"}), 400
        
        data["Prompt"] = data["Prompt"] + " Lagt till i python"
        print(data)
        
        # Startar en Thread som kör något separat
        threading.Thread(target=delayed_post_prompt_url, args=(data["Id"],)).start()
        
        return jsonify(data), 201
    except Exception as e:
        print(f"Error: {e}")
        return jsonify({"error": "Internal Server Error"}), 500



def delayed_post_prompt_url(id):
    time.sleep(10)
    post_prompt_url(id)




def post_prompt_url(id):
    url = "https://localhost:7194/api/PromptUrl"
    
    postData = {
        "promptId": id,
        "url": "C:/Users/Local/Finbild/Haha"
    }
    
    try:
        response = requests.post(url, json=postData, verify=False)
        if response.status_code == 200:
            print("Post request successful!")
            # print("Response data:", response.json())
        else:
            print(f"Failed with status code: {response.status_code}")
            print("Error:", response.text)
    except Exception as e:
        print(f"Error in PostPromptUrl: {e}")

if __name__ == "__main__":
    app.run(debug=True)
