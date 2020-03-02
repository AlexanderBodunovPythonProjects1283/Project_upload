# -*- coding: utf-8 -*-
import pydot
import sys
sys.path.append('C:/Users/nick/Desktop/my/PycharmProjects/behavior_Neo4j/triggers/')
import cv2
import sys
import os
sys.path.append('C:/Users/nick/Desktop/my/PycharmProjects/behavior_Neo4j/triggers/')
sys.path.append('C:/Temp/PycharmProjects/treehouse_system/')
sys.path.append('C:/Temp/files/builded/')
os.environ['PATH'] += os.pathsep + 'C:/Program Files (x86)/Graphviz2.38/bin/'
from graph_current import links_sub
import time
from builded import i1
from builded import i2
from builded import i3
from builded import i4
from builded import i5
from builded import i6
from builded import i7
from builded import i8
from builded import i9
from builded import i10
from builded import i11
from builded import i12
priority=[9,3,4,2,9,1,5,6,8,7,2,3,0]
cals=[0,0,0,0,0,0,0,0,0,0,0,0,0]
c=[i1.i1,i2.i2,i3.i3,i4.i4,i5.i5,i6.i6,i7.i7,i8.i8,i9.i9,i10.i10,i11.i11,i12.i12,0]
G_sub1=Graph(6,links_sub,event_sweet_handler)
G_sub1.visualize("Graph_appear")

while True:
    time.sleep(0.1)
    points=[a*b for a,b in zip(priority,cals)]
    client_id=points.index(max(points))

    d = c[client_id]()
    print(d)

    with open("C:/Temp/files/result_input.txt","w")as file:
        file.write(d)

    G_sub1.change_state("Graph_appear")
    G_sub1.make_output()
    if G_sub1.current_state == 6 + 1:
        G_sub1.current_state = 0

    os.system('"C:\Program Files (x86)\eSpeak\command_line\espeak" -v en Helllo')

    for i in range(len(cals)):
        if i!=client_id:
            cals[i]+=1
        else:
            cals[i]=0