import simpy
import datetime
import random
from datetime import timedelta

def linear_increase(now, min_duration, max_duration):
	percent = now/1300000.0
	result = min_duration + percent*max_duration
	return result

def linear_decrease(now, min_duration, max_duration):
	percent = (1300000.0-now)/1300000.0
	result = min_duration + percent*max_duration
	return result

random_duration = lambda x,min_duration,max_duration : random.gauss((max_duration-min_duration)/2, (max_duration-min_duration)/(6))
increasing_duration = lambda x,min_duration,max_duration : linear_increase(x,min_duration,max_duration)
decreasing_duration = lambda x,min_duration,max_duration : linear_decrease(x,min_duration,max_duration)

start = datetime.datetime.now()
end_time = datetime.datetime.now() + timedelta(hours=24*14)
events = []

def user(env, name, duration, min_duration, max_duration):
	yield env.timeout(5)
	current_time = start
	print("Current Time: %s" % current_time.hour)
	while current_time < end_time:
		current_time = start + timedelta(seconds=env.now)
		if(current_time.hour < 17 and current_time.hour > 8):
			print("[%d] %s event at %s" % (env.now, name, current_time))
			events.append({'name':name,'at':current_time, 'type':'tick'})
			yield env.timeout(duration(env.now, min_duration, max_duration))
		else:
			yield env.timeout(15*60)

env = simpy.Environment()
env.process(user(env, 'rhino', decreasing_duration, 30, 1200))
env.process(user(env, 'cheetah', increasing_duration, 600, 2000))
env.process(user(env, 'moose', random_duration, 200, 6000))
env.process(user(env, 'lion', increasing_duration, 400, 3000))
env.process(user(env, 'mouse', decreasing_duration, 50, 1000))
env.process(user(env, 'hippo', increasing_duration, 900, 4000))
env.process(user(env, 'giraffe', random_duration, 1000,10000))
env.run()

print("Total Events: %d" % len(events))
report = {}
for e in events:
	key = "%d%d%d" % (e['at'].year, e['at'].month, e['at'].day)
	name = e['name']
	if key not in report:
		report[key] = {}
	if name not in report[key]:
		report[key][name] = 0
	report[key][name] += 1

for k in sorted(report.keys()):
	print("%s" % k)
	for name in sorted(report[k].keys()):
		print("  %s | %d" % (name, report[k][name]))

print("Data has been written to: data.csv")		

str = ","
fieldNames = ['feature', 'type','at']
with open('data.csv', 'w') as f:
	f.write(str.join(fieldNames) + "\n")	
	for row in events:
		fields = []		
		fields.append(row['name'])
		fields.append(row['type'])
		fields.append(row['at'].strftime("%Y-%m-%dT%H:%M:%S"))
		f.write(str.join(fields) + "\n")			
