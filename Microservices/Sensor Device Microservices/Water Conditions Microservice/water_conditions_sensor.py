from datetime import datetime
import time
import requests
import random
import pandas

data_microservice_location = "data-microservice"  #localhost 172.17.0.5
data_microservice_port = "80"          #80 58332

def two_digit_representation(value):
    if value < 10:
        return "0" + str(value)
    else:
        return str(value)


def convert_date_to_string(date):
    return str(date.year) + "-" + two_digit_representation(date.month) + "-" + two_digit_representation(date.day) + "T" \
           + two_digit_representation(date.hour) + ":" + two_digit_representation(date.minute) + ":" + \
           two_digit_representation(date.second)

class WavesConditionsData:
    def __init__(self, beach_name, temperature, turbidity, transducer_depth):
        self.beachName = beach_name
        self.temperature = temperature
        self.turbidity = turbidity
        self.transducerDepth = transducer_depth
        timestamp = datetime.fromtimestamp(time.time())
        self.timestamp = convert_date_to_string(timestamp)

    def printData(self):
        print("Timestamp: ", self.timestamp)
        print("     Beach name: " + self.beachName)
        print("     Temperature: ", str(self.temperature))
        print("     Turbidity: ", str(self.turbidity))
        print("     Transducer depth: " + str(self.transducerDepth) )

class DataCollection:
    def __init__(self, metadata):
        self.metadata = metadata

    def send_data(self, record_data):
            url = "http://" + data_microservice_location + ":" + data_microservice_port \
                + "/api/data/water-conditions-sensor" 
            values = record_data.__dict__
            headers = {"Content-type": "application/json"}
            r = requests.post(url, json=values, headers=headers)
            print(r.status_code)
            print("Sending")

    def collect_data(self):
        time.sleep(50)
        filename = "water_conditions_sensor_data.csv"  
        n = sum(1 for line in open(filename)) - 1
        s = 1
        while True:
            skip = sorted(random.sample(range(1, n + 1), n - s))
            df = pandas.read_csv(filename, skiprows=skip)
            water_data = WavesConditionsData(df.values[0][0], float(df.values[0][1]), float(df.values[0][2]), float(df.values[0][3]))
            water_data.printData()
            self.send_data(water_data)
            time.sleep(self.metadata.get_metadata()["sampling_interval"])