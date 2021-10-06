import json
import threading
import flask
from flask_cors import CORS, cross_origin
from metadata import Metadata
from waves_sensor import DataCollection

metadata = Metadata()
data_collection = DataCollection(metadata)

thread = threading.Thread(target=data_collection.collect_data)
thread.start()

app = flask.Flask(__name__)
cors = CORS(app)


@app.route("/get-metadata", methods=['GET'])
@cross_origin()
def return_metadata():
    return metadata.get_metadata()


@app.route("/change-sampling-interval", methods=['PUT'])
@cross_origin()
def change_sample_time():
    new_sample_time = float(flask.request.args['sample-time'])
    metadata.set_sample_time(new_sample_time)
    metadata.save_metadata()
    return metadata.get_metadata()

@app.route("/apply-command", methods=['PUT'])
@cross_origin()
def apply_command():
    message = flask.request.args['CommandMessage']
    beachName = flask.request.args['BeachName']
    print(beachName,  ": ", message)
    return json.dumps({'success':True}), 200, {'ContentType':'application/json'} 

app.run()