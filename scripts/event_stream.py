
import sys
import uuid
import datetime
import time
import requests

if len(sys.argv) <= 2:
	sys.exit("Invalid arguments.  Expected feature name and time between ticks.\r\nUsage: python event_stream.py feature n")

feature = sys.argv[1]
delta = int(sys.argv[2])


while(True):
	uid = uuid.uuid1()
	at = datetime.datetime.now()
	payload = {'Id':str(uid),'Feature':feature,'Type':'tick','At':at.strftime("%Y-%m-%dT%H:%M:%S")}
	r = requests.post("http://localhost:53266/api/UserEvents", data=payload)
	print(r.text)
	time.sleep(delta)