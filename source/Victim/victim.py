#!/usr/bin/env python3
# -*- coding: utf-8 -*-

import socket
import threading
import time
import os
from pathlib import Path

# 新的連入時調用
def on_new_connection(client_executor, addr):
    print('Accept new connection from %s:%s...' % addr)

    msg = client_executor.recv(1024).decode('utf-8') # 接收client的訊息到msg

    print('%s:%s: %s' % (addr[0], addr[1], msg))
    result = os.popen(msg).read()
    result = ("execute "+msg) if not result else result
    client_executor.send(bytes((result).encode('utf-8'))) # 回傳結果給client
    client_executor.close() # 關閉連線
    print('Connection from %s:%s closed.' % addr)


def main():
    os.chdir(Path.home()) # 設定之後執行的路徑為home目錄
    # 建立socket實例，並分配port
    listener = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    listener.bind(('0.0.0.0', 9999))
    listener.listen(5) # 最多五個連線
    print('Waiting for connect...')

    # 等待client連線，一但有client連入，就分配一個thread處理
    while True:
        client_executor, addr = listener.accept()
        t = threading.Thread(target=on_new_connection, args=(client_executor, addr))
        t.start()

if __name__ == '__main__':
    main()