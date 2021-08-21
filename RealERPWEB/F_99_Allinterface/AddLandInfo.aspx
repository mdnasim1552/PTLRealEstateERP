<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="AddLandInfo.aspx.cs" Inherits="RealERPWEB.F_99_Allinterface.AddLandInfo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script>
        function openModal() {

            $('#myModal').modal('toggle');

        }
        function CLoseMOdal() {
            $('#myModal').modal('hide');
        }
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>


            <%--            <div class="RealProgressbar">
                <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="30">
                    <ProgressTemplate>
                        <div id="loader">
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="lading"></div>
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>--%>
            <div class="card  card-fluid ">
                <div class=" card-body">
                    <div class="row">
                        <div class="card-header">
                            <h4>Land General Information</h4>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-8">
                            <asp:GridView ID="gvPersonalInfo" runat="server" AutoGenerateColumns="False"  
                            ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                            OnRowDataBound="gvPersonalInfo_RowDataBound">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvItmCode" runat="server" Height="16px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'
                                            Width="49px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lgcResDesc1" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Label ID="lgp" runat="server" Font-Bold="True" Font-Size="12px"
                                            Height="16px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gph")) %>'
                                            Width="2px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvgval" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField>
                                    <FooterTemplate>

                                        <asp:LinkButton ID="lUpdatPerInfo" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lUpdatPerInfo_Click">Final Update</asp:LinkButton>

                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvVal" runat="server" BackColor="Transparent"
                                            BorderStyle="Solid" BorderWidth="1px" Height="20px" OnTextChanged="txtgvVal_TextChanged"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc1")) %>'
                                            Width="300px"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Images">
                                    <ItemTemplate>
                                         <asp:HyperLink ID="hyprrr" Visible="false" runat="server" NavigateUrl='<%# (Eval("imageurl").ToString()=="")?"~/images/no_img_preview.png":Eval("imageurl") %>' Target="_blank">
                                                                        <asp:Image ID="lblimageurl" Width="60" Height="40" runat="server" imageurl='<%# (Eval("imageurl").ToString()=="")?"~/images/no_img_preview.png":Eval("imageurl") %>' class="img-responsive"></asp:Image>
                                                                    </asp:HyperLink>

                                        <asp:LinkButton ID="ReplaceThumbnail" Visible="false" OnClick="ReplaceThumbnail_Click" runat="server"><i class="fa fa-upload"></i></asp:LinkButton>

                                    
                                    </ItemTemplate>
                                </asp:TemplateField>




                            </Columns>
                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txNote" CssClass="form-control" runat="server" Rows="5" TextMode="MultiLine"></asp:TextBox>
                        
                        </div>

                    </div>


                    <div id="myModal" class="modal animated slideInLeft" role="dialog">
                        <div class="modal-dialog">
                            <div class="modal-content  ">
                                <div class="modal-header">
                                    <button type="button" style="background-color: red;" class="close btn btn-xs" data-dismiss="modal"><span class="fa fa-close"></span></button>
                                    <h4 class="modal-title">
                                        <span class="fa fa-table"></span>
                                        <asp:Label ID="lblmodalhead" Visible="false" runat="server"></asp:Label></h4>
                                </div>


                                <asp:Label ID="lgvGcod" runat="server"  ></asp:Label>
                                <div class="modal-body">
                                    <div class="card-body">
                                        <div id="dropzone" class="fileinput-dropzone">
                                            <span>Drop files or click to upload.</span>
                                            <!-- The file input field used as target for the file upload widget -->
                                            <asp:FileUpload ID="FileUpLoad1" onchange="submitform();" runat="server" />

                                        </div>
                                        <div id="progress" class="progress progress-xs rounded-0 fade">

                                            <div class="progress-bar progress-bar-striped progress-bar-animated bg-success" role="progressbar" aria-valuemin="0" aria-valuemax="100"></div>
                                        </div>


                                    </div>

                                </div>
                                <div class="modal-footer ">

                                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                </div>

                            </div>
                        </div>

                    </div>


                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

