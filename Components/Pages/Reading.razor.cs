﻿using Microsoft.AspNetCore.Components;

namespace se_24.Components.Pages
{
    public partial class Reading : ComponentBase
    {
        [Parameter]
        public int level { get; set; } = 1;

        public int taskTimer = 60;
        public bool isStartScreen = true;
        public bool isReadingScreen = false;
        public bool isQuestionsScreen = false;
        public bool isEndScreen = false;
        public bool isNextButtonEnabled = false;
        public bool isEndButtonEnabled = false;

        public int questionNumber = 1;
        public int numberOfQuestions = 5;

        public bool isButtonsDisabled = true;

        public string[] answers;
        public string[] questions;
        public int[] correctAnswers;

        public string question = "";
        public string text = "";
        public string answer1 = "";
        public string answer2 = "";
        public string answer3 = "";
        public string answer4 = "";

        public int readingTime { get; set; } = 60;
        public int score = 0;
        public string correct = "";

        // Function to initialize the component
        protected override async Task OnInitializedAsync()
        {
            await Task.Delay(500);
            switch (level)
            {
                case 1:
                    numberOfQuestions = 3;
                    answers = new string[numberOfQuestions * 4];
                    questions = new string[numberOfQuestions];
                    correctAnswers = new int[numberOfQuestions];
                    text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam quis neque lacus. Nulla ultrices eget mauris a venenatis. Fusce nec egestas magna, vitae consequat quam. Morbi sodales tellus id arcu convallis cursus. Phasellus semper viverra euismod.";
                    questions[0] = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam quis neque lacus.";
                    answers[0] = "A. Lorem ipsum dolor sit amet";
                    answers[1] = "B. consectetur adipiscing elit";
                    answers[2] = "C. Nam quis neque lacus";
                    answers[3] = "D. Nulla facilisi";
                    correctAnswers[0] = 1;

                    questions[1] = "Nulla ultrices eget mauris a venenatis. Fusce nec egestas magna, vitae consequat quam.";
                    answers[4] = "A. Nulla ultrices eget mauris a venenatis";
                    answers[5] = "B. Fusce nec egestas magna";
                    answers[6] = "C. vitae consequat quam";
                    answers[7] = "D. Curabitur mauris nisi";
                    correctAnswers[1] = 4;

                    questions[2] = "Morbi sodales tellus id arcu convallis cursus. Phasellus semper viverra euismod.";
                    answers[8] = "A. Morbi sodales tellus id arcu convallis cursus";
                    answers[9] = "B. Phasellus semper viverra euismod";
                    answers[10] = "C. Nulla iaculis sapien sit amet vehicula consequat";
                    answers[11] = "D. Vivamus luctus condimentum vulputate";
                    correctAnswers[2] = 3;
                    break;
            }
        }

        // Function to start the reading level
        public async Task OnStartClick()
        {
            isStartScreen = false;
            isReadingScreen = true;
            await StartTimer(readingTime);
        }

        // Function to start the timer
        public async Task StartTimer(int readingTime)
        {
            taskTimer = readingTime;

            while (taskTimer > 0)
            {
                if (!isReadingScreen)
                    break;
                taskTimer--;
                StateHasChanged();
                await Task.Delay(1000);
            }

            if (isReadingScreen)
                OnReadingFinished();
        }

        // Function to finish reading
        public void OnReadingFinished()
        {
            PrepareQuestion();
            isReadingScreen = false;
            isQuestionsScreen = true;
            isButtonsDisabled = false;
        }

        // Function to handle answer click
        public void AnswerClick(int answerNumber)
        {
            if (answerNumber == correctAnswers[questionNumber - 1])
            {
                score++;
                correct = "Correct!";
            }
            else
            {
                correct = "Incorrect!";
            }

            if (questionNumber >= numberOfQuestions)
            {
                isEndButtonEnabled = true;
            }
            else
            {
                isNextButtonEnabled = true;
            }
            isButtonsDisabled = true;
        }

        // Function to move to the next question
        public void OnNextQuestion()
        {
            questionNumber++;
            PrepareQuestion();
            isNextButtonEnabled = false;
            isButtonsDisabled = false;
        }

        // Function to prepare the question
        public void PrepareQuestion()
        {
            if (questionNumber <= numberOfQuestions)
            {
                question = questions[questionNumber - 1];
                answer1 = answers[4 * (questionNumber - 1)];
                answer2 = answers[4 * (questionNumber - 1) + 1];
                answer3 = answers[4 * (questionNumber - 1) + 2];
                answer4 = answers[4 * (questionNumber - 1) + 3];
            }
        }

        // Function to end the level
        public void OnEndLevel()
        {
            isQuestionsScreen = false;
            isEndScreen = true;
        }

        // Function to restart the game
        public void OnRestartClick()
        {
            isEndScreen = false;
            isStartScreen = true;
            isEndButtonEnabled = false;
            score = 0;
            taskTimer = readingTime;
            questionNumber = 1;
        }
    }
}
