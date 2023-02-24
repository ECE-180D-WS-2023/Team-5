import cv2
# Load the cascade classifier
face_cascade = cv2.CascadeClassifier('haarcascade_frontalface_default.xml')

# Open the webcam
cap = cv2.VideoCapture(0)

# Initialize the previous face position
prev_x = 0
left = 0
right = 0

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

        # Determine the direction of movement
        if x > (prev_x + 20):
            direction = 'right'
            left = left+1
        elif x < (prev_x -20):
            direction = 'left'
            right = right+1
        else:
            direction = 'stay'
        # Update the previous face position
        prev_x = x
        
        # Display the direction of movement
        cv2.putText(frame, direction, (x, y-10), cv2.FONT_HERSHEY_SIMPLEX, 0.5, (0, 255, 0), 2)

    # Display the frame with detected faces and direction of movement
    cv2.imshow('Face Detection', frame)
    print('left = ', left)
    print('right = ', right)
    # Wait for a key press to exit
    if cv2.waitKey(1) == ord('q'):
        break

# Release the resources
cap.release()
cv2.destroyAllWindows()

