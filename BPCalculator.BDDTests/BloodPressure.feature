  Feature: Blood Pressure Category Calculator
  
  As someone checking my blood pressure
  I want to receive a category based on my readings
  So that I can understand the result of my measurement

  Scenario: A reading within the ideal range is classified correctly
    Given I have a systolic blood pressure reading of 115
    And I have a diastolic blood pressure reading of 75
    When I request my blood pressure classification
    Then my blood pressure category should be Ideal

  Scenario: User receives a high blood pressure category
    Given the user has entered a systolic reading of 135
    And the user has entered a diastolic reading of 90
    When the user calculates their blood pressure category
    Then the result should be High
  
  
  # Checks that a reading below the ideal range is identified as low.
  Scenario: A lower-than-normal reading is identified correctly
    Given I have a systolic blood pressure reading of 85
    And I have a diastolic blood pressure reading of 55
    When I request my blood pressure classification
    Then my blood pressure category should be Low
