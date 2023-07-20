import { useState } from 'react';
import { useColorScheme, Image } from 'react-native';
import { StatusBar } from 'expo-status-bar';
import { StyledContainer, SafeAreaContainer, StyledMessagebox } from './components/elements/styledContainer';
import { 
  useFonts, 
  Urbanist_400Regular, 
  Urbanist_500Medium, 
  Urbanist_600SemiBold, 
  Urbanist_700Bold, 
  Urbanist_500Medium_Italic,
  Urbanist_600SemiBold_Italic,
  Urbanist_700Bold_Italic 
} from '@expo-google-fonts/urbanist'
import Mimi from './assets/images/mimiChan/mimi_4.png';
import { StyledText } from './components/elements/styledTypography';
import Greeting from './components/greeting/Greeting';

export default function App() {
  const [colorScheme, setColorScheme] = useState(useColorScheme());

  let [fontsLoaded] = useFonts({
    Urbanist_400Regular,
    Urbanist_500Medium,
    Urbanist_600SemiBold,
    Urbanist_700Bold,
    Urbanist_500Medium_Italic,
    Urbanist_600SemiBold_Italic,
    Urbanist_700Bold_Italic
  });

  if (!fontsLoaded) {
    return null;
  }
  return (
    <SafeAreaContainer theme={colorScheme}>
      <StatusBar />
      {/* <StyledContainer theme={colorScheme}>
        <Image source={Mimi} resizeMethod='resize' style={{width: 100, height: 100}} resizeMode='contain'/>
        <StyledMessagebox theme={colorScheme}>
          <StyledText theme={colorScheme}>
            Hello!
          </StyledText>
        </StyledMessagebox>
      </StyledContainer> */}
      <Greeting theme={colorScheme}/>
    </SafeAreaContainer>
  );
}