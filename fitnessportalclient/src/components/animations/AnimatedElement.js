import { useInView } from "react-intersection-observer";
import "./AnimatedElement.css";

function AnimatedElement({ children, animationType, base }) {
  const [ref, inView] = useInView({
    triggerOnce: true,
  });
  console.log('animationType: ', animationType);

  return (
    <div ref={ref} className={`${base} ${inView ? animationType : ''}`}>
      {children}
    </div>
  );
}

export default AnimatedElement