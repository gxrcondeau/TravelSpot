import Button from "../elements/button/Button";
import { StyledTitle } from "../elements/styledTypography";
import { StyledGreetingContainer } from "./styledGreeting";

const Greeting = ({theme}) => {
    return(
        <StyledGreetingContainer theme={theme}>
            <StyledTitle>
                Welcome back, Space Cowboy!
            </StyledTitle>
            <Button theme={theme} text='Login'/>
            <Button theme={theme} text='Sign up'/>
        </StyledGreetingContainer>
    )
}

export default Greeting;