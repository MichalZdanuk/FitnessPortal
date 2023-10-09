export const formatBMIScore = (bmiScore) => {
  if (isNaN(bmiScore)) {
    return 'invalid'
  }

  return bmiScore.toFixed(2)
}
