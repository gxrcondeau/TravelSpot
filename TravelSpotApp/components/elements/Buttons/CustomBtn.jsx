import Icon from 'react-native-vector-icons/AntDesign';

import { ButtonWrapper, ButtonText } from './styledButton';

const CustomBtn = ({title, action, type, icon, halign, p, iconColor, disabled, color, onPress}) => {
    return(
        <ButtonWrapper
            p={p}
            halign={halign}
            onPress={action}
            type={type}
            icon={icon}
            disabled={disabled ? disabled : false}
            onPressOut={onPress}
        >
            {title &&
                <ButtonText
                    type={type}
                    color={color}
                >
                    {title}
                </ButtonText> 
            }
            {
                icon && <Icon name={icon} size={24} color={iconColor}/>
            }
        </ButtonWrapper>
    )
}

export default CustomBtn;