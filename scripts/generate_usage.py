import simpy
import datetime
import random
from datetime import timedelta

def working_hours(current_time):
	return current_time.hour < 17 and current_time.hour > 8

def linear_increase(now, min_duration, max_duration):	
	end_of_time = 60*60*24*365
	time_left = end_of_time - now	
	return min_duration + max_duration *  time_left / end_of_time

def linear_decrease(now, min_duration, max_duration):
	end_of_time = 60*60*24*365	
	return min_duration + max_duration *  now / end_of_time

random_duration = lambda x,min_duration,max_duration : random.gauss((max_duration-min_duration)/2, (max_duration-min_duration)/(6))
increasing_duration = lambda x,min_duration,max_duration : linear_increase(x,min_duration,max_duration)
decreasing_duration = lambda x,min_duration,max_duration : linear_decrease(x,min_duration,max_duration)

start = datetime.datetime.now() - timedelta(weeks=52)
end_time = datetime.datetime.now()
events = []

def user(env, name, duration, min_duration, max_duration):
	yield env.timeout(30)
	current_time = start
	while current_time < end_time:
		current_time = start + timedelta(seconds=env.now)
		if(working_hours(current_time)):
			events.append({'name':name,'at':current_time, 'type':'tick'})
			duration_in_s = duration(env.now, min_duration, max_duration)
			while(duration_in_s < 0):
				duration_in_s = duration(env.now, min_duration, max_duration)			
			yield env.timeout(duration_in_s)
		else:
			yield env.timeout(15*60)

env = simpy.Environment()
#env.process(user(env, 'rhino', decreasing_duration, 30, 1200))
env.process(user(env, 'cheetah', increasing_duration, 60*2, 60*60*4))
env.process(user(env, 'moose', random_duration, 200, 6000))
env.process(user(env, 'lion', increasing_duration, 60*5, 60*60*1))
#env.process(user(env, 'mouse', decreasing_duration, 50, 1000))
env.process(user(env, 'hippo', increasing_duration, 60*60*1, 60*60*6))
env.process(user(env, 'giraffe', random_duration, 1000,10000))
env.run()

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
print("Data has been written to: data.csv")		