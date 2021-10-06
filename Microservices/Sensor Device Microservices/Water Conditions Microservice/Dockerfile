FROM python:3.8.7
WORKDIR /sensor-device-microservice
ENV FLASK_APP=app.py
ENV FLASK_RUN_HOST=0.0.0.0
COPY requirements.txt requirements.txt
COPY water_conditions_sensor_data.csv water_conditions_sensor_data.csv
RUN pip install -r requirements.txt
EXPOSE 5000
COPY . .
CMD ["flask", "run"]