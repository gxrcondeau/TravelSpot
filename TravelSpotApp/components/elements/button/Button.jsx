import { Icon } from "react-native-vector-icons/Ionicons";

import { StyledButtonText, StyledButtonWrapper } from "./styledButton";
import { StyledText } from "../styledTypography";

const Button = ({text, icon, theme}) => {
    return(
        <StyledButtonWrapper theme={theme}>
            {
                text && 
                <StyledText theme={theme}>
                    {text}
                </StyledText>
            }
            {
                icon && 
                <Icon name={icon}/>
            }
        </StyledButtonWrapper>
    )
}

export default Button;