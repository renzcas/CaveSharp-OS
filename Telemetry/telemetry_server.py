#!/usr/bin/env python3
import asyncio
import json
import random
import websockets

PORT = 8765

async def handler(ws, path):
    print("Client connected")
    try:
        while True:
            payload = {
                "tick": random.randint(0, 99999),
                "threat": random.randint(0, 100),
                "creatures": random.randint(1, 50),
                "heat": random.randint(0, 100)
            }
            await ws.send(json.dumps(payload))
            await asyncio.sleep(0.5)
    except websockets.exceptions.ConnectionClosed:
        print("Client disconnected")

async def main():
    async with websockets.serve(handler, "0.0.0.0", PORT):
        print(f"Telemetry server running on ws://0.0.0.0:{PORT}")
        await asyncio.Future()

if __name__ == "__main__":
    asyncio.run(main())
