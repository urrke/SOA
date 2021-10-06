import json
import multiprocessing

sensor = "waves_sensor"
sampling = 10
metadataFile = "waves_sensor_metadata.json"
lock = multiprocessing.Lock()


class Metadata:
    def __init__(self):
        self.metadata = {}
        metadata_file = None
        try:
            metadata_file = open(metadataFile)
            self.metadata = json.load(metadata_file)["metadata"]
        except FileNotFoundError:
            metadata_file = open(metadataFile, "w")
            data = {"metadata": {
                    "sensor": sensor,
                    "sampling_interval": sampling
                    }}
            self.metadata = data["metadata"]
            json.dump(data, metadata_file)
        finally:
            metadata_file.close()
            print(self.metadata)

    def get_metadata(self):
        return self.metadata

    def save_metadata(self):
        metadata_file = open(metadataFile, "w")
        json.dump({"metadata": self.metadata}, metadata_file)
        metadata_file.close()

    def set_sample_time(self, value):
        lock.acquire()
        self.metadata["sampling_interval"] = value
        lock.release()