import cv2
import paho.mqtt.client as mqtt
import numpy as np

# Load the cascade classifier
face_cascade = cv2.CascadeClassifier('haarcascade_frontalface_default.xml')

addToFile = 0

# 0. define callbacks - functions that run when events happen.
# The callback for when the client receives a CONNACK response from the server.
def on_connect(client, userdata, flags, rc):
    print("Connection returned result: " + str(rc))
    # Subscribing in on_connect() means that if we lose the connection and
    # reconnect then subscriptions will be renewed.
    client.subscribe("gamep2/test", qos=1)
    client.publish("gamep1/test", 0, qos=1)

def on_disconnect(client, userdata, rc):
    if rc != 0:
        print("Unexpected Disconnect")
    else:
        print("Expected Disconnect")


def fileToArray(f):
    content = []
    for line in f:
        content.append(line)
    return content

def arrayshift(a, newline):
    a.append(newline)
    return a[1:]

def arraytostring(a):
    string = ''
    for line in a:
        string = string + line
    return string

def sendToUnity(filename, data, buffersize):
    datapacket = data
    sent = False
    while not sent:
        try:
            fil = open(filename, "r+")
            file_append = open('file_append.txt', 'a')
        except:
            print('Could not access file. try sending it again!')
            continue
        lines = fileToArray(fil)
        if len(lines) < buffersize:
            newtext = arraytostring(lines)+datapacket
            fil.seek(0)
            fil.write(newtext)
        else:
            newlines = arrayshift(lines, datapacket)
            newtext = arraytostring(newlines)
            fil.seek(0)
            fil.truncate(0)
            fil.write(newtext)
        file_append.write(datapacket)
        file_append.close()
        fil.close()
        sent = True

    
open("/Users/sraavyapradeep/Downloads/HopefulFixTennisGame/Assets/Scripts/facetrack.txt", "w").close()
open("file_append.txt", "w").close()



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


# The default message callback.
# (you can create separate callbacks per subscribed topic)
def on_message(client, userdata, message):
    #print("Received message: " + str(message.payload) + " on topic " + message.topic + " with QoS " + str(message.qos))
    # put information here!!
    comp = int(message.payload)
    #client.publish("gamep1/test", 0, qos=1)
# client = mqtt.Client()
# client.on_connect = on_connect
# client.on_disconnect = on_disconnect
# client.on_message = on_message
# client.connect_async("mqtt.eclipseprojects.io")
# client.loop_start()


# Open the webcam
cap = cv2.VideoCapture(0)

# Initialize the previous face position
prev_x = 0
left = 0
right = 0
xVal = 0

count = 0

while True:
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
        count = count + 1

        if (count % 5 == 0):
            

            # Determine the direction of movement
            '''if x > (prev_x + 20):
                direction = 'right'
                left = left+1
            elif x < (prev_x -20):
                direction = 'left'
                right = right+1
            else:
                direction = 'stay' 
            # Update the previous face position
            prev_x = x '''
            #direction = 'stay'

            xVal = x
            print('x = ' + str(xVal))
            if (addToFile % 1 == 0):
                # WRITE TO FILE HERE
                sendToUnity('facetracking.txt', str(xVal) + '\n', 5)
            addToFile+=1

        
        # Display the direction of movement
        cv2.putText(frame, 'stay', (x, y-10), cv2.FONT_HERSHEY_SIMPLEX, 0.5, (0, 255, 0), 2)

    # Display the frame with detected faces and direction of movement
    cv2.imshow('Face Detection', frame)
    # print('left = ', left)
    # print('right = ', right)
    
    # Wait for a key press to exit
    if cv2.waitKey(1) == ord('q'):
        break

while True: # perhaps add a stopping condition using some break or something.
    pass # do your non-blocked other stuff here, like receive IMU data or something.
# use subscribe() to subscribe to a topic and receive messages.
# use publish() to publish messages to the broker.
# use disconnect() to disconnect from the broker.
# client.loop_stop()
# client.disconnect()
# Release the resources
cap.release()
cv2.destroyAllWindows()