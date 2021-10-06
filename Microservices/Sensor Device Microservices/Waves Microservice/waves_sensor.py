from datetime import datetime
import time
import requests
import random
import pandas

data_microservice_location = "data-microservice"  
data_microservice_port = "80" 


def two_digit_representation(value):
    if value < 10:
        return "0" + str(value)
    else:
        return str(value)


def convert_date_to_string(date):
    return str(date.year) + "-" + two_digit_representation(date.month) + "-" + two_digit_representation(date.day) + "T" \
           + two_digit_representation(date.hour) + ":" + two_digit_representation(date.minute) + ":" + \
           two_digit_representation(date.second)


class WavesSensorData:
    def __init__(self, beach_name, wave_height, wave_period, sensor_battery_life):
        self.beachName = beach_name
        self.waveHeight = wave_height
        self.wavePeriod = wave_period
        self.batteryLife = sensor_battery_life
        timestamp = datetime.fromtimestamp(time.time())
        self.timestamp = convert_date_to_string(timestamp)

    def printData(self):
        print("Timestamp: ", self.timestamp)
        print("     Beach name: " + self.beachName)
        print("     Wave height: ", str(self.waveHeight))
        print("     Wave period: ", str(self.wavePeriod))
        print("     Sensor battery life: " + str(self.batteryLife)+ "\n" )
        


class DataCollection:
    def __init__(self, metadata):
        self.metadata = metadata

    def send_data(self, record_data):
        url = "http://" + data_microservice_location + ":" + data_microservice_port \
              + "/api/data/waves-sensor"
        values = record_data.__dict__
        headers = {"Content-type": "application/json"}
        r = requests.post(url, json=values, headers=headers)
        print(r.status_code)
        print("Sending")

    def collect_data(self):
        time.sleep(50)
        filename = "waves_sensor_data.csv"
        n = sum(1 for line in open(filename)) - 1
        s = 1
        while True:
            skip = sorted(random.sample(range(1, n + 1), n - s))
            df = pandas.read_csv(filename, skiprows=skip)
            water_data = WavesSensorData(df.values[0][0], float(df.values[0][1]), int(df.values[0][2]), float(df.values[0][3]))
            water_data.printData()
            self.send_data(water_data)
            time.sleep(self.metadata.get_metadata()["sampling_interval"])