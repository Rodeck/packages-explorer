export interface ApiResponse<T>
{
    resource: T[];
    success: boolean;
}