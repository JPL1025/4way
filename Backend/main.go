package main

import (
	"encoding/json"
	"fmt"
	"log"
	"net/http"
	"time"
)

func main() {
	http.HandleFunc("/", func(w http.ResponseWriter, r *http.Request) {})
}

// JSON responses for different days of the year
var jsonResponses = map[int]interface{}{
	1:   map[string]string{"message": "Happy New Year!"},
	2:   map[string]string{"message": "Second day of the year!"},
	365: map[string]string{"message": "Last day of the year!"},
	// Add more responses as needed
}

func main() {
	http.HandleFunc("/get-json", jsonHandler)
	fmt.Println("Initializing on port 8080")
	log.Fatal(http.ListenAndServe(":8080", nil))
}

func jsonHandler(w http.ResponseWriter, r *http.Request) {
	dayOfYear := getDayOfYear() // Gets day of the year

	response, exists := jsonResponses[dayOfYear]
	if !exists {
		response = map[string]string{"message": "The day of year is a regular day"}
	}

	w.Header().Set("Content-Type", "application/json")
	json.NewEncoder(w).Encode(response)
}

func getDayOfYear() int {
	now := time.Now()
	return now.YearDay()
}
