#pragma once

template <typename Vector2Type>
class Vec2 {
    public:
    Vector2Type x, y;
};
#define literal_vec2(type, x, y) (Vec2<type>){ x, y }
#define literal_1tovec2(type, i) (Vec2<type>){ i, i }

template <typename Vector3Type>
class Vec3 {
    public:
    Vector3Type x, y, z;
};
#define literal_vec3(type, x, y, z) (Vec3<type>){ x, y, z }
#define literal_1tovec3(type, i) (Vec3<type>){ i, i, i }

template <typename Vector4Type>
class Vec4 {
    public:
    Vector4Type w, h, x, y;
};
#define literal_vec4(type, w, h, x, y) (Vec4<type>){ w, h, x, y }
#define literal_1tovec4(type, i) (Vec4<type>){ i, i, i, i }
#define literal_vec4tosdlrect(vec4obj) (SDL_Rect) {(i32)vec4obj.w, (i32)vec4obj.h, (i32)vec4obj.x, (i32)vec4obj.y}