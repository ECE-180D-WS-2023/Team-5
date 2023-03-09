import cv2
import paho.mqtt.client as mqtt
import numpy as np
# Load the cascade classifier
face_cascade = cv2.CascadeClassifier('haarcascade_frontalface_default.xml')



# 0. define callbacks - functions that run when events happen.
# The callback for when the client receives a CONNACK response from the server.
def on_connect(client, userdata, flags, rc):
    print("Connection returned result: " + str(rc))
    client.subscribe("gamep1/test", qos=1)
# Subscribing in on_connect() means that if we lose the connection and
# reconnect then subscriptions will be renewed.
# client.subscribe("ece180d/test")
# The callback of the client when it disconnects.
def on_disconnect(client, userdata, rc):
    if rc != 0:
        print("Unexpected Disconnect")
    else:
        print("Expected Disconnect")
# The default message callback.
# (won’t be used if only publishing, but can still exist)

def getFaceValue():
    xVal = 0
    #count = 0
    # Read a frame from the webcam
    ret, frame = cap.read()

    # Convert the frame to grayscale
    gray = cv2.cvtColor(frame, cv2.COLOR_BGR2GRAY)

    # Detect faces in the grayscale image
    faces = face_cascade.detectMultiScale(gray, scaleFactor=1.1, minNeighbors=5, minSize=(30, 30))

    # Process each detected face
    for (x, y, w, h) in faces:
        # Draw a rectangle around the face
        cv2.rectangle(frame, (x, y), (x+w, y+h), (0, 255, 0), 2)
        #count = count + 1

        # if (count % 10 == 0):

        #     # Determine the direction of movement
        #     '''if x > (prev_x + 20):
        #         direction = 'right'
        #         left = left+1
        #     elif x < (prev_x -20):
        #         direction = 'left'
        #         right = right+1
        #     else:
        #         direction = 'stay' 
        #     # Update the previous face position
        #     prev_x = x '''
        #     #direction = 'stay'
        #     print("here")
        #     xVal = x
        #     #print('x = ' + str(xVal))
        
        xVal = x
        print('x = ' + str(xVal))

        # Display the direction of movement
        cv2.putText(frame, 'stay', (x, y-10), cv2.FONT_HERSHEY_SIMPLEX, 0.5, (0, 255, 0), 2)

    # Display the frame with detected faces and direction of movement
    cv2.imshow('Face Detection', frame)
    # print('left = ', left)
    # print('right = ', right)
    print(xVal)
    return xVal


def on_message(client, userdata, message):
    print("Received message: " + str(message.payload) + " on topic " + message.topic + " with QoS " + str(message.qos))
    #print(str(message.payload))
    client.publish("gamep2/test", int(getFaceValue()), qos=1)
client = mqtt.Client()
client.on_connect = on_connect
client.on_disconnect = on_disconnect
client.on_message = on_message
client.connect_async("mqtt.eclipseprojects.io")
client.loop_start()

# Open the webcam
cap = cv2.VideoCapture(0)

# Initialize the previous face position
prev_x = 0
left = 0
right = 0


# while True:
    # Read a frame from the webcam
    # ret, frame = cap.read()

    # # Convert the frame to grayscale
    # gray = cv2.cvtColor(frame, cv2.COLOR_BGR2GRAY)

    # # Detect faces in the grayscale image
    # faces = face_cascade.detectMultiScale(gray, scaleFactor=1.1, minNeighbors=5, minSize=(30, 30))


    # # Process each detected face
    # for (x, y, w, h) in faces:
    #     # Draw a rectangle around the face
    #     cv2.rectangle(frame, (x, y), (x+w, y+h), (0, 255, 0), 2)
    #     count = count + 1

    #     if (count % 10 == 0):

    #         # Determine the direction of movement
    #         '''if x > (prev_x + 20):
    #             direction = 'right'
    #             left = left+1
    #         elif x < (prev_x -20):
    #             direction = 'left'
    #             right = right+1
    #         else:
    #             direction = 'stay' 
    #         # Update the previous face position
    #         prev_x = x '''
    #         #direction = 'stay'
    #         xVal = x
    #         print('x = ' + str(xVal))
        
    #     # Display the direction of movement
    #     cv2.putText(frame, 'stay', (x, y-10), cv2.FONT_HERSHEY_SIMPLEX, 0.5, (0, 255, 0), 2)

    # # Display the frame with detected faces and direction of movement
    # cv2.imshow('Face Detection', frame)
    # # print('left = ', left)
    # # print('right = ', right)
    
    # # Wait for a key press to exit
    # if cv2.waitKey(1) == ord('q'):
    #     break

print("Publishing...")
while True: # perhaps add a stopping condition using some break or something.
    pass
# 6. use disconnect() to disconnect from the broker.
client.loop_stop()
client.disconnect()
# Release the resources
cap.release()
cv2.destroyAllWindows()

