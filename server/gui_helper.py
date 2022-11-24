from PyQt6.QtGui import QFont
from PyQt6.QtWidgets import QLabel, QPushButton

from server.analoggaugewidget import AnalogGaugeWidget 
from server.gradiant_progressbar import GradiantProgressBar

class GuiHelper():
    def create_gauge(parent, x=None ,y=None) -> AnalogGaugeWidget:
        gauge = AnalogGaugeWidget(parent)
        gauge.set_enable_fine_scaled_marker(False)
        gauge.set_enable_big_scaled_grid(False)
        gauge.set_enable_ScaleText(False)
        gauge.set_enable_CenterPoint(False)
        gauge.set_enable_Needle_Polygon(False)
        gauge.set_enable_barGraph(False)
        gauge.set_start_scale_angle(165)
        gauge.set_total_scale_angle_size(210)
        gauge.set_gauge_color_inner_radius_factor(600)
        gauge.set_MaxValue(100)
        if x is not None and y is not None:
            gauge.setGeometry(x, y, 130, 130)
        else:
            gauge.resize(130, 130)
        gauge.update_value(50)
        gauge.set_DisplayValueColor(0, 255, 255)
        return gauge

    def create_label(parent, x=None, y=None, width=None, height=None, text="", image=None, font_size=15, color="#FFFFFF") -> QLabel:
        label = QLabel(parent)
        label.setText(text)
        font = QFont("Decorative", font_size)
        font.setBold(True)
        label.setFont(font)
        label.setStyleSheet("color: %s;" % color)

        if image is not None:
            label.setStyleSheet(label.styleSheet() + " background-image: url(%s);background-repeat:no-repeat;" % image)
        
        if (width is None or height is None) and x is not None and y is not None:
            label.move(x, y)
        elif width is not None and height is not None and x is not None and y is not None:
            label.setGeometry(x, y, width, height)

        return label

    def create_progressbar(parent, x, y, width, height) -> GradiantProgressBar:
        progress = GradiantProgressBar(parent)
        progress.setFormat("")
        progress.setValue(50)
        progress.setMaximum(100)
        progress.setGeometry(x, y, width, height)
        return progress
    
    def create_button(parent, x, y, width, height, image, click=None, press=None, release=None, checkable=False):
        button = QPushButton(parent)
        button.setCheckable(checkable)
        if checkable:
            pressed_image = image.replace(".", "_pressed.")
            stre = "QPushButton {border-image: url(server/resource/" + image + ");} " \
                 + "QPushButton:checked {border-image: url(server/resource/" + pressed_image + ");}"
            button.setStyleSheet(stre)
        else:
            button.setStyleSheet("border-image: url(server/resource/" + image + ");")
        
        if click is not None:
            button.clicked.connect(click)
        if press is not None:
            button.pressed.connect(press)
        if release is not None:
            button.released.connect(release)

        if (width is None or height is None) and x is not None and y is not None:
            button.move(x, y)
        elif width is not None and height is not None and x is not None and y is not None:
            button.setGeometry(x, y, width, height)

        button.setFlat(True)
        return button